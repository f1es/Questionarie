using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQuestionnaire.MVVM.Models.Interfaces
{
    public interface IResult
    {
        public string ResultMessage { get; set; }
        public string Header { get; set; }
        public string BackgroundSource { get; set; }
    }
}
