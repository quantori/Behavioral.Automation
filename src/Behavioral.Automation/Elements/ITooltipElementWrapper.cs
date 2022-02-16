namespace Behavioral.Automation.Elements
{
    public interface ITooltipElementWrapper : IWebElementWrapper
    {
        /// <summary>
        /// Message that appears when mouse is hovered over element
        /// </summary>
        public string Tooltip { get; }
    }
}