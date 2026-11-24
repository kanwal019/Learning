using System.Threading;
using System.Threading.Tasks;

namespace Library.Common.Services;

public interface IServiceHost
{
    void Run();
    Task RunAsync(CancellationToken cancellationToken = default);
}
