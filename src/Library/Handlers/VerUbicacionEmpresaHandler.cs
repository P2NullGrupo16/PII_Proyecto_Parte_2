using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace ClassLibrary
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsability que implementa el comando "/verubicacionempresa".
    /// </summary>
    public class VerUbicacionEmpresaHandler: BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase.
        /// Esta clase procesa el mensaje ingresado por el usuario.
        /// </summary>
        /// <param name="client">Recibe por parametro el bot de origen.</param>
        /// <param name="next">Recibe por parametro el siguiente Handler.</param>
        /// <returns></returns>
        public VerUbicacionEmpresaHandler(TelegramBotClient client, BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/verubicacionempresa"};
            this.bot = client;
        }
        private TelegramBotClient bot;

        /// <summary>
        /// Este método procesa el mensaje "/verubicacion" y retorna true.
        /// En caso contrario retorna false.
        /// </summary>
        /// <param name="mensaje">Recibe por parametro el mensaje a procesar.</param>
        /// <param name="respuesta">Recibe por parametro la respuesta al mensaje procesado.</param>
        /// <returns>Retorna true si se ha podido realizar la operación, o false en caso contrario.</returns>
        protected override bool InternalHandle(IMensaje mensaje, out string respuesta)
        {
            if (!this.ChequearHandler(mensaje, "/verubicacionempresa"))
            {
                respuesta = string.Empty;
                return false;
            }
            else if (Singleton<ContenedorPrincipal>.Instancia.HistorialDeChats[mensaje.Id].ComprobarUltimoComandoIngresado("/verubicacionempresa") == true)
            {
                List<string> listaConParametros = Singleton<ContenedorPrincipal>.Instancia.HistorialDeChats[mensaje.Id].BuscarUltimoComando("/verubicacionempresa");
                if (Singleton<ContenedorPrincipal>.Instancia.Empresas.ContainsKey(mensaje.Id))
                {                       
                    Direccion(mensaje);

                    respuesta = "";
                    return true;
                }
                else
                {
                    respuesta = $"Usted no es una empresa, no puede usar este comando.";
                    return true;
                }
            }
            
            respuesta = string.Empty;
            return false;
        }       

        /// <summary>
        /// Este método utiliza la dirección del emprendedor para encontrar su ubicacion con la LocationApi.
        /// Las imagenes de ubicacion obtenidas las almacena en una carpeta por nombre del usuario.
        /// </summary>
        /// <param name="mensaje">Recibe por parametro el mensaje a procesar.</param>
        /// <returns></returns>
        public async Task Direccion(IMensaje mensaje)
        {
            Empresa value = Singleton<ContenedorPrincipal>.Instancia.Empresas[mensaje.Id];
            string direccion = value.Ubicacion.NombreCalle;
            LocationApiClient client = new LocationApiClient();

            // Utilizando el mensaje ingresado como parametro. 
            Location direccionActual = await client.GetLocationAsync(direccion);
            await client.DownloadMapAsync(direccionActual.Latitude, direccionActual.Longitude,@$"..\UbicacionesMaps\ubicacion{value.Nombre}.png");

            // Este método se utiliza para poder inviable el mensaje con el mapa al usuario.
            SendProfileImage(mensaje);
        }
        
        private async Task SendProfileImage(IMensaje mensaje)
        {
            // Can be null during testing
            Empresa value = Singleton<ContenedorPrincipal>.Instancia.Empresas[mensaje.Id];
            
            await bot.SendChatActionAsync(mensaje.Id, ChatAction.UploadPhoto);
            string filePath = @$"..\UbicacionesMaps\ubicacion{value.Nombre}.png";
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();
            await bot.SendPhotoAsync(chatId: mensaje.Id, photo: new InputOnlineFile(fileStream, fileName),caption: $"Direccion de la Empresa. {OpcionesUso.AccionesEmpresas()}");
        }
    }
}