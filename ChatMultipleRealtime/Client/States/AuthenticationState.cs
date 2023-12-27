using ChatMultipleRealtime.Shared.DTOs;
using System.ComponentModel;

namespace ChatMultipleRealtime.Client.States
{
    public class AuthenticationState : INotifyPropertyChanged
    {
        public const string AuthStoreKey = "authkey";

        public event PropertyChangedEventHandler? PropertyChanged;
        //public int Id { get; set; }
        //public string? Name { get; set; }
        public UserDto User { get; set; } = default;
        public string? Token { get; set; }
        private bool _IsAuthenticated;
        public bool IsAuthenticated
        {
            get
            {
                return _IsAuthenticated;
            }
            set
            {
                if (_IsAuthenticated != value)
                {
                    _IsAuthenticated = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAuthenticated)));
                }
            }
        }


        public void LoadState(AuthResponseDto authResponseDto)
        {
            User = authResponseDto.User;
            Token = authResponseDto.Token;
            IsAuthenticated = true;
        }
        public void UnLoadState( )
        {
            User = default;
            Token = null;
            IsAuthenticated = false;
        }
    }
}
