﻿namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface ISdkSesionService
{
    bool ConexionInciada { get; }
    bool SesionUsuarioIniciada { get; }
    bool EmpresaAbierta { get; }
    string BaseDatos { get; }
    void IniciarConexion();
    void TerminarConexion();
    void IniciarSesionUsuario();
    void IniciarSesionUsuario(string nombreUsuario, string contrasena);
    void AbrirEmpresa(string nombreBaseDatos);
    void CierraEmpresa();
}
