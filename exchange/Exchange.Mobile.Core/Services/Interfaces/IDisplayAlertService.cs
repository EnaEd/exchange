using System;

namespace Exchange.Mobile.Core.Services.Interfaces
{
    public interface IDisplayAlertService
    {
        void ShowAlert(string message, string title, string okbtnText, Action okBtnAction);
        void ShowToast(string message);
    }
}
