using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Core.Application.ContpaqiContabilidad.Models;
using SDKCONTPAQNGLib;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Services;

public sealed class SdkSesionService : ISdkSesionService
{
    private readonly TSdkSesion _sdkSesion;

    public SdkSesionService(TSdkSesion sdkSesion)
    {
        _sdkSesion = sdkSesion;
    }

    public bool CanAbrirEmpresa => EmpresaAbierta == false;
    public bool CanCerrarEmpresa => EmpresaAbierta;
    public string BaseDatos { get; private set; } = string.Empty;
    public bool EmpresaAbierta { get; private set; }

    public bool ConexionInciada { get; private set; }

    public bool SesionUsuarioIniciada { get; private set; }

    public void IniciarConexion()
    {
        if (_sdkSesion.conexionActiva == 0)
            _sdkSesion.iniciaConexion();

        if (_sdkSesion.conexionActiva == 1)
            ConexionInciada = true;
    }

    public void TerminarConexion()
    {
        if (ConexionInciada)
        {
            _sdkSesion.finalizaConexion();
            ConexionInciada = false;
        }
    }

    public void IniciarSesionUsuario()
    {
        _sdkSesion.firmaUsuario();

        if (_sdkSesion.ingresoUsuario == 1)
            SesionUsuarioIniciada = true;
    }

    public void IniciarSesionUsuario(string nombreUsuario, string contrasena)
    {
        _sdkSesion.firmaUsuarioParams(nombreUsuario, contrasena);

        if (_sdkSesion.ingresoUsuario == 1)
            SesionUsuarioIniciada = true;
    }

    public void AbrirEmpresa(string nombreBaseDatos)
    {
        if (CanAbrirEmpresa)
        {
            int sdkResult = _sdkSesion.abreEmpresa(nombreBaseDatos);

            if (sdkResult == SdkResult.Success)
            {
                EmpresaAbierta = true;
                BaseDatos = nombreBaseDatos;
            }
        }
    }

    public void CierraEmpresa()
    {
        if (CanCerrarEmpresa)
        {
            _sdkSesion.cierraEmpresa();
            EmpresaAbierta = false;
            BaseDatos = string.Empty;
        }
    }
}
