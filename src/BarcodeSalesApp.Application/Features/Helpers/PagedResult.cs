namespace BarcodeSalesApp.Application.Features.Helpers;

public class PagedResult<T>
{
  public IList<T> Items { get; set; } = [];
  public int TotalCount { get; set; }
}