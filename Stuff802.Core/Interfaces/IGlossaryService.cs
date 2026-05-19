using Stuff802.Core.Models;

namespace Stuff802.Core.Interfaces;

public interface IGlossaryService
{
    IEnumerable<GlossaryEntryDto> GetGlossaryEntries();
}
