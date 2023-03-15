using CORE.Account.Application;
using CORE.Account.Application.Interfaces;
using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using CORE.Account.Exception;
using CORE.Account.Helpers;
using CORE.Account.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace TEST.TestProject
{
    public class Movimientos
    {
        private Mock<ICuentasRepository> cuentasRepository = new Mock<ICuentasRepository>();
        private Mock<IMovimientosRepository> movimientosRepository = new Mock<IMovimientosRepository>();
        private Mock<IClientesRepository> clientesRepository = new Mock<IClientesRepository>();
        private Mock<IDateTimeProvider> dateTimeProvider = new Mock<IDateTimeProvider>();
        private Mock<IDbContextTransaction> _transaction= new Mock<IDbContextTransaction>();
        private decimal saldoActual;

        [SetUp]
        public void Setup()
        {
            
            
        }
        private void InitMoq(
            DateTime fecha,
            int numeroCuenta = 1000,            
            int clienteId = 1,      
            ETipoMovimiento tipoMovimiento = ETipoMovimiento.Credito,
            decimal valorCredito = 250,
            decimal saldoActualCuenta = 2000,
            decimal limiteRetiros = 1000,
            EEstadoCuenta estadoCuenta= EEstadoCuenta.Activo      ,
            decimal totalRetiros=0
        )
        {
            this.saldoActual = saldoActualCuenta;
            dateTimeProvider
              .Setup(x => x.GetCurrentTime())
              .Returns(fecha);

            movimientosRepository
              .Setup(x => x.RegistrarMovimiento(numeroCuenta, fecha, ETipoMovimiento.Credito, Math.Abs(valorCredito), saldoActualCuenta + Math.Abs(valorCredito)))
              .ReturnsAsync(new MMovimiento(numeroCuenta,fecha, ETipoMovimiento.Credito, Math.Abs(valorCredito), saldoActualCuenta + Math.Abs(valorCredito)));

            movimientosRepository
              .Setup(x => x.RegistrarMovimiento(numeroCuenta, fecha, ETipoMovimiento.Debito, Math.Abs(valorCredito)*-1, saldoActualCuenta - Math.Abs(valorCredito)))
              .ReturnsAsync(new MMovimiento(numeroCuenta,fecha, ETipoMovimiento.Debito, Math.Abs(valorCredito)*-1, saldoActualCuenta - Math.Abs(valorCredito)));

            cuentasRepository
                .Setup(x => x.ObtenerSaldoCuenta(numeroCuenta))
                .ReturnsAsync(saldoActualCuenta);

            cuentasRepository
               .Setup(x => x.ObtenerCuenta(numeroCuenta))
               .ReturnsAsync(new MCuenta(numeroCuenta, clienteId, 100, estadoCuenta));

            movimientosRepository
                .Setup(x=> x.ObtenerTotalRetiros(clienteId, fecha))
                 .ReturnsAsync(totalRetiros);

            clientesRepository
                .Setup(x => x.ObtenerLimiteRetiro(clienteId))
                .ReturnsAsync(limiteRetiros);

            CancellationToken asdr = default;
            _transaction
                 .Setup(x => x.CommitAsync(asdr))
                 .Returns(Task.CompletedTask);

            
        }
        [Test]
        public async Task RegistrarCreditoAcuentaDeshabilitada()
        {
            int numeroCuenta = 1000;
            decimal saldoActual = 2000;
            decimal valorCredito = 250;

            InitMoq(
                fecha: DateTime.Now,
                numeroCuenta ,
                clienteId: 1,
                tipoMovimiento: ETipoMovimiento.Credito,
                valorCredito,
                saldoActual,
                limiteRetiros: 1000,
                estadoCuenta: EEstadoCuenta.Inactivo
            );

            ICuentasService cuentasService = new CuentasService(cuentasRepository.Object, movimientosRepository.Object, clientesRepository.Object, dateTimeProvider.Object, _transaction.Object);
            try
            {
                var movimiento = await cuentasService.RegistrarCredito(numeroCuenta, valorCredito);
            }
            catch (CuentaException ex)
            {
                Assert.Pass($"No se puede realizar el deposito: {ex.Message}");                
            }
            Assert.Fail();
        }


        [Test]
        public async Task RegistrarCreditoAcuentaHabilitada()
        {

            int numeroCuenta = 1000;
            decimal saldoActual = 2000;
            decimal valorCredito = 250;
            InitMoq(
                fecha: DateTime.Now,
                numeroCuenta,
                clienteId: 1,
                tipoMovimiento: ETipoMovimiento.Credito,
                valorCredito,
                saldoActual,
                limiteRetiros: 1000,
                estadoCuenta: EEstadoCuenta.Activo
            );

            ICuentasService cuentasService = new CuentasService(cuentasRepository.Object, movimientosRepository.Object, clientesRepository.Object, dateTimeProvider.Object, _transaction.Object);


            var movimiento = cuentasService.RegistrarCredito(numeroCuenta, valorCredito);
            Assert.That(saldoActual + Math.Abs(valorCredito), NUnit.Framework.Is.EqualTo(movimiento?.Result.Saldo));


        }
        [Test]
        public async Task RegistrarDebitoConSaldoSuficiente()
        {
            int numeroCuenta = 1000;
            decimal saldoActual = 2000;
            decimal valorCredito = 250;
            InitMoq(
                fecha: DateTime.Now,
                numeroCuenta,
                clienteId: 1,
                ETipoMovimiento.Debito,
                valorCredito,
                saldoActual,
                limiteRetiros: 1000,
                estadoCuenta: EEstadoCuenta.Activo
            );

            ICuentasService cuentasService = new CuentasService(cuentasRepository.Object, movimientosRepository.Object, clientesRepository.Object, dateTimeProvider.Object, _transaction.Object);


            var movimiento = await cuentasService.RegistrarDebito(numeroCuenta, valorCredito);
            Assert.That(saldoActual - Math.Abs(valorCredito), Is.EqualTo(movimiento.Saldo));
        }

        [Test]
        public async Task RegistrarDebitoSinSaldoSuficiente()
        {
            int numeroCuenta = 1000;
            decimal saldoActual = 0;
            decimal valorCredito = 250;
            InitMoq(
                fecha: DateTime.Now,
                numeroCuenta,
                clienteId: 1,
                ETipoMovimiento.Debito,
                valorCredito,
                saldoActual,
                limiteRetiros: 1000,
                estadoCuenta: EEstadoCuenta.Activo
            );

            ICuentasService cuentasService = new CuentasService(cuentasRepository.Object, movimientosRepository.Object, clientesRepository.Object, dateTimeProvider.Object, _transaction.Object);

            try
            {
                var movimiento = await cuentasService.RegistrarDebito(numeroCuenta, valorCredito);

            }
            catch (CuentaException ex)
            {

                Assert.Pass($"No se puede realizar el retiro: {ex.Message}");
            }
            Assert.Fail();

        }

        [Test]
        public async Task RegistrarDebitoConLimiteExcedido()
        {
            int numeroCuenta = 1000;
            decimal saldoActual = 2000;
            decimal valorCredito = 250;
            InitMoq(
                fecha: DateTime.Now,
                numeroCuenta,
                clienteId: 1,
                ETipoMovimiento.Debito,
                valorCredito,
                saldoActual,
                limiteRetiros: 1000,
                estadoCuenta: EEstadoCuenta.Activo,
                totalRetiros: 950  
              );

            ICuentasService cuentasService = new CuentasService(cuentasRepository.Object, movimientosRepository.Object, clientesRepository.Object, dateTimeProvider.Object, _transaction.Object);

            try
            {
                var movimiento = await cuentasService.RegistrarDebito(numeroCuenta, valorCredito);

            }
            catch (CuentaException ex)
            {

                Assert.Pass($"No se puede realizar el retiro: {ex.Message}");
            }
            Assert.Fail();
        }
    }
}