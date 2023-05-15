using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CourseProjectLiteMK5.TagHelpers
{
    [HtmlTargetElement(Attributes = "is-active-page")]
    public class ActivePageTagHelper : TagHelper
    {
        /// <summary>The name of the action method.</summary>
        /// <remarks>Must be <c>null</c> if <see cref="P:Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper.Route" /> is non-<c>null</c>.</remarks>
        [HtmlAttributeName("asp-page")]
        public string Page { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.ViewContext" /> for the current request.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (ShouldBeActive())
            {
                MakeActive(output);
            }

            output.Attributes.RemoveAll("is-active-page");
        }

        private bool ShouldBeActive()
        {
            string currentPage = ViewContext?.RouteData.Values["Page"]?.ToString();

            if (!string.IsNullOrWhiteSpace(Page) && Page.ToLower() != currentPage?.ToLower())
            {
                return false;
            }

            return true;
        }

        private void MakeActive(TagHelperOutput output)
        {
            bool isDarkMode = false;

            if (!isDarkMode)
            {
                var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
                if (classAttr == null)
                {
                    if (classAttr?.Value.ToString()?.IndexOf("hdr") > 0)
                    {
                        classAttr = new TagHelperAttribute("class", "active border-bottom border-dark");
                        output.Attributes.Add(classAttr);
                    } 
                    else if (classAttr?.Value.ToString()?.IndexOf("ftr") > 0)
                    {
                        classAttr = new TagHelperAttribute("class", "active border-bottom border-light");
                        output.Attributes.Add(classAttr);
                    }
                    else
                    {
                        classAttr = new TagHelperAttribute("class", "active border-bottom");
                        output.Attributes.Add(classAttr);
                    }
                }
                else if (classAttr.Value == null || classAttr.Value.ToString()?.IndexOf("active") < 0)
                {
                    if (classAttr.Value?.ToString()?.IndexOf("hdr") > 0)
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value == null
                        ? "active border-bottom border-dark"
                        : classAttr.Value.ToString() + " active border-bottom border-dark");
                    }
                    else if (classAttr.Value?.ToString()?.IndexOf("ftr") > 0)
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value == null
                        ? "active border-bottom border-light"
                        : classAttr.Value.ToString() + " active border-bottom border-light");
                    }
                    else
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value == null
                        ? "active border-bottom"
                        : classAttr.Value.ToString() + " active border-bottom");
                    }
                }
                else
                {
                    if (classAttr.Value.ToString()?.IndexOf("hdr") > 0)
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value.ToString() + " border-bottom border-dark");
                    }
                    else if (classAttr.Value.ToString()?.IndexOf("ftr") > 0)
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value.ToString() + " border-bottom border-light");
                    }
                    else
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value.ToString() + " border-bottom");
                    }
                }
            }
            else
            {
                var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
                if (classAttr == null)
                {
                    if (classAttr?.Value.ToString()?.IndexOf("hdr") > 0)
                    {
                        classAttr = new TagHelperAttribute("class", "active border-bottom border-light");
                        output.Attributes.Add(classAttr);
                    }
                    else if (classAttr?.Value.ToString()?.IndexOf("ftr") > 0)
                    {
                        classAttr = new TagHelperAttribute("class", "active border-bottom border-dark");
                        output.Attributes.Add(classAttr);
                    }
                    else
                    {
                        classAttr = new TagHelperAttribute("class", "active border-bottom");
                        output.Attributes.Add(classAttr);
                    }
                }
                else if (classAttr.Value == null || classAttr.Value.ToString()?.IndexOf("active") < 0)
                {
                    if (classAttr.Value?.ToString()?.IndexOf("hdr") > 0)
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value == null
                        ? "active border-bottom border-light"
                        : classAttr.Value.ToString() + " active border-bottom border-light");
                    }
                    else if (classAttr.Value?.ToString()?.IndexOf("ftr") > 0)
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value == null
                        ? "active border-bottom border-dark"
                        : classAttr.Value.ToString() + " active border-bottom border-dark");
                    }
                    else
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value == null
                        ? "active border-bottom"
                        : classAttr.Value.ToString() + " active border-bottom");
                    }
                }
                else
                {
                    if (classAttr.Value.ToString()?.IndexOf("hdr") > 0)
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value.ToString() + " border-bottom border-light");
                    }
                    else if (classAttr.Value.ToString()?.IndexOf("ftr") > 0)
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value.ToString() + " border-bottom border-dark");
                    }
                    else
                    {
                        output.Attributes.SetAttribute("class", classAttr.Value.ToString() + " border-bottom");
                    }
                }
            }
        }
    }
}
