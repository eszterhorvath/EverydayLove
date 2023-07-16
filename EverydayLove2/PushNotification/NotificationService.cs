using Plugin.LocalNotification;

namespace EverydayLove2.PushNotification
{
    public static class NotificationService
	{
		public async static Task InitializeAsync()
		{
            if (!await LocalNotificationCenter.Current.AreNotificationsEnabled())
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }

			var request = new NotificationRequest
			{
				Title = "Elolvastad már a mai üzid? :)",
				Description = "Ma is nagyon szeretünk!",
				Schedule = new NotificationRequestSchedule
				{
					NotifyTime = DateTime.Today.AddHours(20),
                    RepeatType = NotificationRepeat.Daily
				}
            };

            await LocalNotificationCenter.Current.Show(request);
        }
	}
}

