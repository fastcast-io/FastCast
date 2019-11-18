using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastCast.Pages
{
    public class AnswerSuccessModel : PageModel
    {
        private readonly IFastCastService _fastCastService;

        public AnswerSuccessModel(IFastCastService fastCastService)
        {
            _fastCastService = fastCastService;
        }
        public void OnGet()
        {
            String sessionCode = _fastCastService.GetData("SessionCode");
            if (sessionCode != "-1")
            {
                ViewData["SessionCode"] = sessionCode;
            }
        }
    }
}