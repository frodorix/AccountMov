using CORE.Account.Application;
using CORE.Account.Application.Interfaces;
using CORE.Account.Domain.Enum;
using CORE.Account.Domain.Model;
using CORE.Account.Exception;
using CORE.Account.Interfaces;
using Moq;

namespace TEST.TestProject
{
    public class Tests
    {
        private Mock<ICuentasRepository> cuentasRepository = new Mock<ICuentasRepository>();
        private Mock<IMovimientosRepository> movimientosRepository = new Mock<IMovimientosRepository>();
        private Mock<IClientesRepository> clientesRepository = new Mock<IClientesRepository>();

        [SetUp]
        public void Setup()
        {
        }
        private void InitMoq(int numeroCuenta = 1000,
        int clienteId = 1,
        DateTime fecha = default,
        ETipoMovimiento tipoMovimiento = ETipoMovimiento.Credito,
        decimal valorCredito = 250,
        decimal saldoActual = 2000,
        decimal limiteRetiros = 1000)
        {
            movimientosRepository
              .Setup(x => x.RegistrarMovimiento(numeroCuenta, fecha, ETipoMovimiento.Credito, valorCredito, saldoActual))
              .ReturnsAsync(new MMovimiento(fecha, tipoMovimiento, Math.Abs(valorCredito), saldoActual + Math.Abs(valorCredito)));

            movimientosRepository
              .Setup(x => x.RegistrarMovimiento(numeroCuenta, fecha, ETipoMovimiento.Debito, valorCredito, saldoActual))
              .ReturnsAsync(new MMovimiento(fecha, tipoMovimiento, Math.Abs(valorCredito), saldoActual - Math.Abs(valorCredito)));

            cuentasRepository
                .Setup(x => x.ObtenerSaldoCuenta(numeroCuenta))
                .ReturnsAsync(saldoActual);

            cuentasRepository
               .Setup(x => x.ObtenerCuenta(numeroCuenta))
               .ReturnsAsync(new MCuenta(numeroCuenta, clienteId, 100, EEstadoCuenta.Inactivo));

            clientesRepository
                .Setup(x => x.ObtenerLimiteRetiro(clienteId))
                .ReturnsAsync(limiteRetiros);
        }
        [Test]
        public async Task RegistrarCreditoAcuentaDeshabilitada()
        {
            int numeroCuenta = 1000;
            decimal saldoActual = 2000;
            decimal valorCredito = 250;
            InitMoq(
                numeroCuenta ,
                clienteId: 1,
                fecha: DateTime.Now,
                tipoMovimiento: ETipoMovimiento.Credito,
                valorCredito,
                saldoActual,
                limiteRetiros: 1000
            );

            ICuentasService cuentasService = new CuentasService(cuentasRepository.Object, movimientosRepository.Object, clientesRepository.Object);
            try
            {
                var movimiento = await cuentasService.RegistrarCredito(numeroCuenta, valorCredito);
                Assert.AreEqual(movimiento.Saldo, saldoActual + Math.Abs(valorCredito));
            }
            catch (CuentaException ex)
            {
                Assert.Pass($"Se encontraron: {ex.Message}");
            }
            Assert.Fail();
        }
        [Test]
        public void RegistrarDebito()
        {
            Assert.Pass();
        }
    }
}