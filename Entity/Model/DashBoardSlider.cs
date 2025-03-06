using System;
using Entity.Common;

namespace Entity.Model
{
	public class DashboardSlider : BaseDbModel
{
    public int Id { get; set; }
    public string DashboardSliderInfo { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string BackgroundImage { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public int OrderKey { get; set; }
}

}

