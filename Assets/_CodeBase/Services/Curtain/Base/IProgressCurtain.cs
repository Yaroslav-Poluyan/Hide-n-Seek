using System;

namespace _CodeBase.Services.Curtain.Base
{
   public interface IProgressCurtain : ICurtain
   {
      Action AnimationsComplete { get; set; }
      void SetProgress(float progress);
      void HideForce();
   }
}