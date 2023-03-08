using CORE.Account.Domain.Model;

namespace CORE.Account.Application.Interfaces
{
    public interface ICuentasService
    {
        Task<MMovimiento> RegistrarCredito(int numeroCuenta, decimal valor);
        Task<MMovimiento> RegistrarDebito(int numeroCuenta, decimal valor);
    }
}