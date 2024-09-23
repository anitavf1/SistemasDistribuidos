
namespace FileDownload;

public class Program{
    public static async Task Main(string[] args){
        var cancellationToken = new CancellationTokenSource();
        var peer = new Peer();
        var task = peer.Start(cancellationToken.Token);

        if(args.Length > 0 && args[0] == "download"){
            await peer.DownloadFile(args[1], Convert.ToInt32(args[2]), args[3], args[4], cancellationToken.Token);
        }else{
            Console.WriteLine("Waiting for other peers to connect...");
        }
        await task;
    }
}