using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastCast.Pages
{
    public class AnswerModel : PageModel
    {
        private readonly IFastCastService _fastCastService;

        public AnswerModel(IFastCastService fastCastService)
        {
            _fastCastService = fastCastService;
        }

        public void OnGet()
        {
            String sessionCode = _fastCastService.GetData("SessionCode");
            if (sessionCode == "-1")
            {
                ViewData["SessionAnswerError"] = "Could not authentify your request. Please re-enter your session code in the home page";
            }
            else
            {
                ViewData["SessionCode"] = sessionCode;
                ViewData["SessionName"] = "SESSION TEST"; //_fastCastService.GetData("SessionName");
                ViewData["SessionFormId"] = _fastCastService.GetData("SessionForm");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Todo: add some more checks and different checks
            return RedirectToPage("./AnswerSuccess");

        }
    }
}
