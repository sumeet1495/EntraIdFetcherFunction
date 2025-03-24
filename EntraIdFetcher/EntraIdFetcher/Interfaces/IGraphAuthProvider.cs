using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace EntraIdFetcher.Interfaces
{
    public interface IGraphAuthProvider
    {
        GraphServiceClient GetGraphServiceClient();
    }
}
