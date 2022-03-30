namespace Oz.Bet.Notification.App;

public interface ISampleHub
{
    Task ChangeOccured(BulletinEntity bulletin);
}