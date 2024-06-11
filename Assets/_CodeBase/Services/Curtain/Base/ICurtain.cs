using System;
using System.Threading.Tasks;

namespace _CodeBase.Services.Curtain.Base
{
    public interface ICurtain
    {
        void Show();
        Task Hide();
        bool IsActive { get; set; }
        Action<bool> OnStateChanged { get; set; }
    }
}