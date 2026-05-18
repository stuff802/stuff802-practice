using Stuff802.Core.Models;

namespace Stuff802.Core.Services;

public interface IGlossaryService
{
    IEnumerable<GlossaryEntryDto> GetGlossaryEntries();
}
