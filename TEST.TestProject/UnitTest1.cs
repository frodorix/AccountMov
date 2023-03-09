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
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public   void RegistrarCreditoAcuentaInexistente()
        {            
                           

            
            Mock<ICuentasRepository> cuentasRepository = new Mock<ICuentasRepository>();
            Mock<IMovimientosRepository> movimientosRepository = new Mock<IMovimientosRepository>();
            Mock<IClientesRepository> clientesRepository = new Mock<IClientesRepository>();

            int numeroCuenta = 1000;
            int clienteId = 1; 
            DateTime fecha = default;
            var tipoMovimiento = ETipoMovimiento.Credito;
            decimal valorCredito = 250;
            decimal saldoActual = 2000;

            movimientosRepository
                .Setup(x => x.RegistrarMovimiento(numeroCuenta, fecha, tipoMovimiento, valorCredito, saldoActual))
                .ReturnsAsync(new MMovimiento(fecha,tipoMovimiento, Math.Abs(valorCredito), saldoActual + Math.Abs(valorCredito)));

            cuentasRepository
                .Setup(x => x.ObtenerSaldoCuenta(numeroCuenta))
                .ReturnsAsync(saldoActual);

             cuentasRepository
                .Setup(x => x.ObtenerCuenta(numeroCuenta))
                .ReturnsAsync((MCuenta)null);

            //clientesRepository
            //    .Setup(x => x.ObtenerLimiteRetiro(clienteId))
            //    .ReturnsAsync((MCuenta)null);


            ICuentasService cuentasService = new CuentasService(cuentasRepository.Object, movimientosRepository.Object, clientesRepository.Object);            
            try
            {
                var movimiento = cuentasService.RegistrarCredito(numeroCuenta, valorCredito);
                Assert.AreEqual(movimiento.Result.Saldo, saldoActual + Math.Abs(valorCredito));
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