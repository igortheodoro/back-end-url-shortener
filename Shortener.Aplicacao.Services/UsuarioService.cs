using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shortener.Aplicacao.Models.ViewModels;
using Shortener.Aplicacao.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Aplicacao.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly IMailService mailService;

        public UsuarioService(UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            IMailService mailService)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.mailService = mailService;
        }

        public async Task<RespostaViewModel> RegistrarUsuarioAsync(CadastrarUsuarioViewModel user)
        {
            if (user == null)
            {
                user.Password = null;
                user.ConfirmPassword = null;

                return new RespostaViewModel
                {
                    Mensagem = "Faltaram informações para registrar o usuário.",
                    Objeto = user,
                    Success = false
                };
            }

            if (user.Password != user.ConfirmPassword)
            {
                user.Password = null;
                user.ConfirmPassword = null;

                return new RespostaViewModel
                {
                    Mensagem = "Sua confirmação de senha está incorreta.",
                    Objeto = user,
                    Success = false
                };
            }

            var usuario = new IdentityUser
            {
                Email = user.Email,
                UserName = user.Email
            };

            var result = await userManager.CreateAsync(usuario, user.Password);

            if (result.Succeeded)
            {
                // Enviar e-mail de confirmação

                var confirmarEmailToken = await userManager.GenerateEmailConfirmationTokenAsync(usuario);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmarEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{configuration["AppUrl"]}/confirmaremail?userid={usuario.Id}&token={validEmailToken}";
                
                string nome = usuario.Email.Substring(0, usuario.Email.IndexOf('@'));

                await mailService.SendEmailAsync(usuario.Email,
                    "Confirme seu e-mail do url-shortener.",
                    $"<!DOCTYPE html><html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'><head> <meta charset='utf-8'> <!-- utf-8 works for most cases --> <meta name='viewport' content='width=device-width'> <!-- Forcing initial-scale shouldn't be necessary --> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <!-- Use the latest (edge) version of IE rendering engine --> <meta name='x-apple-disable-message-reformatting'> <!-- Disable auto-scale in iOS 10 Mail entirely --> <title></title> <!-- The title tag shows in email notifications, like Android 4.4. --> <link href='https://fonts.googleapis.com/css?family=Lato:300,400,700' rel='stylesheet'> <!-- CSS Reset : BEGIN --> <style> /* What it does: Remove spaces around the email design added by some email clients. */ /* Beware: It can remove the padding / margin and add a background color to the compose a reply window. */ html,body {{ margin: 0 auto !important; padding: 0 !important; height: 100% !important; width: 100% !important; background: #f1f1f1;}}/* What it does: Stops email clients resizing small text. */* {{ -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;}}/* What it does: Centers email on Android 4.4 */div[style*='margin: 16px 0'] {{ margin: 0 !important;}}/* What it does: Stops Outlook from adding extra spacing to tables. */table,td {{ mso-table-lspace: 0pt !important; mso-table-rspace: 0pt !important;}}/* What it does: Fixes webkit padding issue. */table {{ border-spacing: 0 !important; border-collapse: collapse !important; table-layout: fixed !important; margin: 0 auto !important;}}/* What it does: Uses a better rendering method when resizing images in IE. */img {{ -ms-interpolation-mode:bicubic;}}/* What it does: Prevents Windows 10 Mail from underlining links despite inline CSS. Styles for underlined links should be inline. */a {{ text-decoration: none;}}/* What it does: A work-around for email clients meddling in triggered links. */*[x-apple-data-detectors], /* iOS */.unstyle-auto-detected-links *,.aBn {{ border-bottom: 0 !important; cursor: default !important; color: inherit !important; text-decoration: none !important; font-size: inherit !important; font-family: inherit !important; font-weight: inherit !important; line-height: inherit !important;}}/* What it does: Prevents Gmail from displaying a download button on large, non-linked images. */.a6S {{ display: none !important; opacity: 0.01 !important;}}/* What it does: Prevents Gmail from changing the text color in conversation threads. */.im {{ color: inherit !important;}}/* If the above doesn't work, add a .g-img class to any image in question. */img.g-img + div {{ display: none !important;}}/* What it does: Removes right gutter in Gmail iOS app: https://github.com/TedGoas/Cerberus/issues/89 *//* Create one of these media queries for each additional viewport size you'd like to fix *//* iPhone 4, 4S, 5, 5S, 5C, and 5SE */@media only screen and (min-device-width: 320px) and (max-device-width: 374px) {{ u ~ div .email-container {{ min-width: 320px !important; }}}}/* iPhone 6, 6S, 7, 8, and X */@media only screen and (min-device-width: 375px) and (max-device-width: 413px) {{ u ~ div .email-container {{ min-width: 375px !important; }}}}/* iPhone 6+, 7+, and 8+ */@media only screen and (min-device-width: 414px) {{ u ~ div .email-container {{ min-width: 414px !important; }}}} </style> <!-- CSS Reset : END --> <!-- Progressive Enhancements : BEGIN --> <style> .primary{{background: #30e3ca;}}.bg_white{{background: #ffffff;}}.bg_light{{background: #fafafa;}}.bg_black{{background: #000000;}}.bg_dark{{background: rgba(0,0,0,.8);}}.email-section{{padding:2.5em;}}/*BUTTON*/.btn{{padding: 10px 15px;display: inline-block;}}.btn.btn-primary{{border-radius: 5px;background: #30e3ca;color: #ffffff;}}.btn.btn-white{{border-radius: 5px;background: #ffffff;color: #000000;}}.btn.btn-white-outline{{border-radius: 5px;background: transparent;border: 1px solid #fff;color: #fff;}}.btn.btn-black-outline{{border-radius: 0px;background: transparent;border: 2px solid #000;color: #000;font-weight: 700;}}h1,h2,h3,h4,h5,h6{{font-family: 'Lato', sans-serif;color: #000000;margin-top: 0;font-weight: 400;}}body{{font-family: 'Lato', sans-serif;font-weight: 400;font-size: 15px;line-height: 1.8;color: rgba(0,0,0,.4);}}a{{color: #30e3ca;}}table{{}}/*LOGO*/.logo h1{{margin: 0;}}.logo h1 a{{color: #30e3ca;font-size: 24px;font-weight: 700;font-family: 'Lato', sans-serif;}}/*HERO*/.hero{{position: relative;z-index: 0;}}.hero .text{{color: rgba(0,0,0,.3);}}.hero .text h2{{color: #000;font-size: 40px;margin-bottom: 0;font-weight: 400;line-height: 1.4;}}.hero .text h3{{font-size: 24px;font-weight: 300;}}.hero .text h2 span{{font-weight: 600;color: #30e3ca;}}/*HEADING SECTION*/.heading-section{{}}.heading-section h2{{color: #000000;font-size: 28px;margin-top: 0;line-height: 1.4;font-weight: 400;}}.heading-section .subheading{{margin-bottom: 20px !important;display: inline-block;font-size: 13px;text-transform: uppercase;letter-spacing: 2px;color: rgba(0,0,0,.4);position: relative;}}.heading-section .subheading::after{{position: absolute;left: 0;right: 0;bottom: -10px;content: '';width: 100%;height: 2px;background: #30e3ca;margin: 0 auto;}}.heading-section-white{{color: rgba(255,255,255,.8);}}.heading-section-white h2{{font-family: line-height: 1;padding-bottom: 0;}}.heading-section-white h2{{color: #ffffff;}}.heading-section-white .subheading{{margin-bottom: 0;display: inline-block;font-size: 13px;text-transform: uppercase;letter-spacing: 2px;color: rgba(255,255,255,.4);}}ul.social{{padding: 0;}}ul.social li{{display: inline-block;margin-right: 10px;}}/*FOOTER*/.footer{{border-top: 1px solid rgba(0,0,0,.05);color: rgba(0,0,0,.5);}}.footer .heading{{color: #000;font-size: 20px;}}.footer ul{{margin: 0;padding: 0;}}.footer ul li{{list-style: none;margin-bottom: 10px;}}.footer ul li a{{color: rgba(0,0,0,1);}}@media screen and (max-width: 500px) {{}} </style></head><body width='100%' style='margin: 0; padding: 0 !important; mso-line-height-rule: exactly; background-color: #f1f1f1;'><center style='width: 100%; background-color: #f1f1f1;'> <div style='display: none; font-size: 1px;max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden; mso-hide: all; font-family: sans-serif;'> &zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp; </div> <div style='max-width: 600px; margin: 0 auto;' class='email-container'> <!-- BEGIN BODY --> <table align='center' role='presentation' cellspacing='0' cellpadding='0' border='0' width='100%' style='margin: auto;'> <tr> <td valign='top' class='bg_white' style='padding: 1em 2.5em 0 2.5em;'> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='100%'> <tr> <td class='logo' style='text-align: center;'> <h1><a href={url}>Confirme seu e-mail.</a></h1> </td> </tr> </table> </td> </tr><!-- end tr --> <tr> <td valign='middle' class='hero bg_white' style='padding: 3em 0 2em 0;'> <img src='https://i.imgur.com/juvlRYg.png' alt='' style='width: 300px; max-width: 600px; height: auto; margin: auto; display: block;'> </td> </tr><!-- end tr --><tr> <td valign='middle' class='hero bg_white' style='padding: 2em 0 4em 0;'> <table> <tr> <td> <div class='text' style='padding: 0 2.5em; text-align: center;'> <h2>Por favor, {nome} verifique seu e-mail do shortener clicando <a href={url}>aqui</a>.</h2> </div> </td> </tr> </table> </td> </tr><!-- end tr --> <!-- 1 Column Text + Button : END --> </table> </td> <td valign='top' width='33.333%' style='padding-top: 20px;'> </td> </tr><!-- end: tr --> </table> </div> </center></body></html>",
                    nome);

                return new RespostaViewModel
                {
                    Mensagem = "Usuário registrado com sucesso!",
                    Objeto = null,
                    Success = true
                };
            }

            user.Password = null;
            user.ConfirmPassword = null;

            return new RespostaViewModel
            {
                Mensagem = "Não foi possível registrar o usuário.",
                Success = false,
                Objeto = user
            };

        }

        public async Task<RespostaViewModel> LoginUsuarioAsync(LoginUsuarioViewModel user)
        {
            var usuarioEncotrado = await userManager.FindByEmailAsync(user.Email);

            if (usuarioEncotrado == null)
            {
                return new RespostaViewModel
                {
                    Mensagem = "Não foi possível logar, verifique seu email e senha.",
                    Objeto = null,
                    Success = false
                };
            }

            var result = await userManager.CheckPasswordAsync(usuarioEncotrado, user.Password);

            if (!result)
            {
                return new RespostaViewModel
                {
                    Mensagem = "Verifique os dados informados. Senha ou email estão incorretos.",
                    Objeto = null,
                    Success = false
                };
            }

            // Gerar os tokens, com os direitos que o usuário tem

            var claims = new[]
            {
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.NameIdentifier, usuarioEncotrado.Id)
            };

            // Configuração do token

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["AuthSettings:Issuer"],
                audience: configuration["AuthSettings:audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenAsString);

            return new RespostaViewModel
            {
                Mensagem = tokenAsString,
                Success = true,
                Objeto = token.ValidTo
            };
        }

        public async Task<RespostaViewModel> ConfirmarEmailAsync(string userId, string token)
        {

            var usuario = await userManager.FindByIdAsync(userId);

            if (usuario == null)
            {
                return new RespostaViewModel
                {
                    Mensagem = "Não foi possível confirmar o email.",
                    Objeto = null,
                    Success = false
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);

            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await userManager.ConfirmEmailAsync(usuario, normalToken);

            if (result.Succeeded)
            {
                return new RespostaViewModel
                {
                    Mensagem = "E-mail confirmado com sucesso!",
                    Objeto = null,
                    Success = true
                };
            }

            return new RespostaViewModel
            {
                Mensagem = "Erro! O e-mail não foi confirmado.",
                Objeto = null,
                Success = false
            };
        }

        public async Task<RespostaViewModel> EsqueceuSenhaAsync(string email)
        {
            var usuario = await userManager.FindByEmailAsync(email);

            if (usuario == null)
            {
                return new RespostaViewModel
                {
                    Mensagem = "Email não encontrado.",
                    Objeto = null,
                    Success = false
                };
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(usuario);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{configuration["AppUrl"]}/redefinirsenha?email={email}&token={validToken}";

            string nome = usuario.Email.Substring(0, usuario.Email.IndexOf('@'));

            await mailService.SendEmailAsync(usuario.Email,
                "Redefinição de senha",
                $"<h4>Olá, <a href='{url}'>clique aqui</a> para redefinir a sua senha.</h4>",
                nome);

            

            return new RespostaViewModel
            {
                Mensagem = "E-mail de redefinição de senha foi enviado com sucesso.",
                Objeto = null,
                Success = true
            };
        }

        public async Task<RespostaViewModel> RedefinirSenhaAsync(RedefinirSenhaViewModel model)
        {
            var usuario = await userManager.FindByEmailAsync(model.Email);

            if (usuario == null)
            {
                return new RespostaViewModel
                {
                    Mensagem = "Usuário não encontrado",
                    Objeto = null,
                    Success = false
                };
            }

            if (model.ConfirmarSenha != model.NovaSenha)
            {
                return new RespostaViewModel
                {
                    Mensagem = "A combinação de senhas não estão corretas.",
                    Objeto = null,
                    Success = false
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await userManager.ResetPasswordAsync(usuario, normalToken, model.NovaSenha);

            if (result.Succeeded)
            {
                return new RespostaViewModel
                {
                    Mensagem = "Senha alterada com sucesso!",
                    Objeto = null,
                    Success = true
                };
            }

            return new RespostaViewModel
            {
                Mensagem = "Não foi possível alterar a senha.",
                Objeto = null,
                Success = false
            };

        }
    }
}
