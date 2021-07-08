using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.ViewComponents
{
    [ViewComponent]
    public class AuthView : ViewComponent
    {
        public IViewComponentResult Invoke(FormType formType)
        {
            if (formType == FormType.SIGN_IN)
            {
                return View("SignInAuthView");
            }
            return View("SignUpAuthView");
        }

        public enum FormType
        {
            SIGN_IN, SIGN_UP
        }
    }
}
