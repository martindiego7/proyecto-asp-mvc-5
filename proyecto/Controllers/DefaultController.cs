using Model;
using proyecto.App_Start;
using proyecto.ViewModels;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace proyecto.Controllers
{
    public class DefaultController : Controller
    {
        private Usuario usuario = new Usuario();
        public ActionResult Index()
        {
            return View(usuario.Obtener(FrontOfficeStartUp.UsuarioVisualizado(), true));
        }

        public JsonResult Enviarcorreo(ContactoViewModel model)
        {
            var rm = new ResponseModel();
            var _usuario = usuario.Obtener(FrontOfficeStartUp.UsuarioVisualizado());

            if (ModelState.IsValid)
            {
                try
                {
                    var mail = new MailMessage();
                    mail.From = new MailAddress(model.Correo, model.Nombre);
                    mail.To.Add("diegonashor@gmail.com");
                    mail.Subject = "correo desde contacto";
                    mail.IsBodyHtml = true;
                    mail.Body = model.Mensaje;

                    var smtpServer = new SmtpClient("smtp.gmail.com");
                    smtpServer.Port = 587;
                    smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpServer.UseDefaultCredentials = false;
                    smtpServer.Credentials = new System.Net.NetworkCredential("diegonashor@gmail.com", "125789463lda");
                    smtpServer.EnableSsl = true;
                    smtpServer.Send(mail);
                }
                catch(Exception e)
                {
                    rm.SetResponse(false, e.Message);
                    return Json(rm);
                    throw;
                }

                rm.SetResponse(true);
                rm.function = "CerrarContacto()";
            }

            return Json(rm);
        }

        public ActionResult ExportaPDF()
        {
            return new ActionAsPdf("PDF");
        }

        public ActionResult PDF()
        {
            return View(usuario.Obtener(FrontOfficeStartUp.UsuarioVisualizado(), true));
        }
    }
}