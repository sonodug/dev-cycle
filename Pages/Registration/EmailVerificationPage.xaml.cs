using System.Windows.Controls;

namespace wpf_game_dev_cycle.Pages;


//email verification происходит при наличии в бд email адреса с соответствующим издателем, потом отправляется код и
//подтверждения, следом из бд вытаскивается рандомный verification code email cоотв. адресу, и отправляется письмом,
//после введения кода он сверяется по базе и дает доступ к приложению
public partial class EmailVerificationPage : Page
{
    public EmailVerificationPage()
    {
        InitializeComponent();
    }
}