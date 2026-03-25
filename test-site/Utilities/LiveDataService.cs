using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace test_site.Utilities;

public class LiveDataTicker : BackgroundService
{
    private readonly IHubContext<LiveHub> _hubContext;
    private List<int[]>? _liveData;

    public LiveDataTicker(IHubContext<LiveHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task StartAsync(CancellationToken token)
    {
        string liveFilePath = Path.Combine(AppContext.BaseDirectory, "LiveData.json");

        string jsonString = await File.ReadAllTextAsync(liveFilePath, token);

        _liveData = JsonSerializer.Deserialize<List<int[]>>(jsonString);

        await base.StartAsync(token);
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        if (_liveData == null || _liveData.Count == 0) return;
        using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

        int currentIndex = 0;
        int totalItems = _liveData.Count;

        while (await timer.WaitForNextTickAsync(token))
        {
            int[] currentNumbers = _liveData[currentIndex];
            var payload = new
            {
                Number1 = currentNumbers[0],
                Number2 = currentNumbers[1],
                Number3 = currentNumbers[2],
            };

            await _hubContext.Clients.All.SendAsync("ReceiveNumbers", payload, token);

            currentIndex = (currentIndex + 1) % totalItems;
        }
    }
}