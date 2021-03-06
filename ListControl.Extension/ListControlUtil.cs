﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;
using System.Text;

namespace ListControl.Extension
{
    internal static class ListControlUtil
    {
        public static MvcHtmlString GenerateHtml(string name, List<SelectListItem> items, ControlType controlType, object stateValue,string cssClass,bool disabled)
        {
            TagBuilder container = new TagBuilder("div");
            container.AddCssClass(cssClass);
            MvcHtmlString result=null;
            switch (controlType)
            {
                case ControlType.CheckBox:
                    IEnumerable<string> currentValues = stateValue as IEnumerable<string>;
                    result= GenerateCheckBoxList(container, name, items, currentValues,disabled);
                    break;
                case ControlType.Radio:
                    string currentValue = stateValue.ToString();
                    result= GenerateRadioButtonList(container, name, items, currentValue,disabled);
                    break;
            }
            return result;
        }

        private static MvcHtmlString GenerateCheckBoxList(TagBuilder container, string name, IEnumerable<SelectListItem> items,
            IEnumerable<string> currentValues, bool disabled)
        {
            int idSuffix = 1;
            foreach (var item in items)
            {
                string controlId = string.Format("{0}{1}", name, idSuffix);
                bool currentItemIschecked = currentValues != null ? currentValues.Any(i => i == item.Value) : false;
                container.InnerHtml += GenerateControl(name, controlId, item.Text, item.Value, currentItemIschecked,"checkbox", disabled);
                idSuffix++;
            }
            return new MvcHtmlString(container.ToString());
        }

        private static MvcHtmlString GenerateRadioButtonList(TagBuilder container, string name, IEnumerable<SelectListItem> items,
            string currentValue, bool disabled)
        {
            int idSuffix = 1;
            foreach (var item in items)
            {
                string controlId = string.Format("{0}{1}", name, idSuffix);
                bool currentItemIschecked = item.Value == currentValue;
                container.InnerHtml += GenerateControl(name, controlId, item.Text, item.Value, currentItemIschecked, "radio", disabled);
                idSuffix++;
            }
            return new MvcHtmlString(container.ToString());
        }

        private static string GenerateControl(string name, string id, string labelText, string value, bool isChecked, string type,bool disabled)
        {
            StringBuilder sb = new StringBuilder();
            TagBuilder label = new TagBuilder("label");
            label.MergeAttribute("for", id);
            label.SetInnerText(labelText);
            TagBuilder input = new TagBuilder("input");
            input.GenerateId(id);
            input.MergeAttribute("name", name);
            input.MergeAttribute("type", type);
            input.MergeAttribute("value", value);
            if (disabled)
            {
                input.MergeAttribute("disabled", "disabledw");
            }
            if (isChecked)
            {
                input.MergeAttribute("checked", "checked");
            }
            sb.AppendLine(input.ToString());
            sb.AppendLine(label.ToString());
            return sb.ToString();
        }
    }
}