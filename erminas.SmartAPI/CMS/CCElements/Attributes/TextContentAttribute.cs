﻿/*
 * Smart API - .Net programatical access to RedDot servers
 * Copyright (C) 2012  erminas GbR 
 *
 * This program is free software: you can redistribute it and/or modify it 
 * under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>. 
 */

using System;
using System.Globalization;
using erminas.SmartAPI.Utils;

namespace erminas.SmartAPI.CMS.CCElements.Attributes
{
    public class TextContentAttribute : RDXmlNodeAttribute
    {
        #region TextType enum

        public enum TextType
        {
            Default = 3,
            Sample = 10
        }

        #endregion

        private readonly TextType _type;

        private Guid _guid;
        private bool _hasChanged;
        private string _text;

        public TextContentAttribute(CCElement parent, TextType type, string name)
            : base(parent, name)
        {
            _type = type;
        }

        public override object DisplayObject
        {
            get { return Text; }
        }

        public string Text
        {
            get
            {
                if (_text == null)
                {
                    var parent = ((CCElement) Parent);
                    var lang = parent.LanguageVariant;
                    _text = _guid == Guid.Empty
                                ? string.Empty
                                : parent.ContentClass.Project.GetTextContent(_guid, lang,
                                                                             ((int) _type).ToString(
                                                                                 CultureInfo.InvariantCulture));
                }
                return _text;
            }
            set
            {
                _text = value;
                _hasChanged = true;
            }
        }

        public override bool IsAssignableFrom(IRDAttribute o, out string reason)
        {
            var attr = o as TextContentAttribute;
            reason = "";
            return attr != null && attr._type == _type;
        }

        public override void Assign(IRDAttribute o)
        {
            var attr = (TextContentAttribute) o;
            Text = attr.Text;
        }

        public void Commit()
        {
            if (!_hasChanged)
            {
                return;
            }
            var parent = ((CCElement) Parent);
            LanguageVariant lang = parent.LanguageVariant;

            if (string.IsNullOrEmpty(_text))
            {
                SetValue(null);
                _text = null;
                return;
            }
            try
            {
                SetValue(
                    parent.ContentClass.Project.SetTextContent(_guid, lang,
                                                               ((int) _type).ToString(CultureInfo.InvariantCulture),
                                                               _text).
                        ToRQLString());
            }
            catch (Exception)
            {
                throw new Exception("could not set " + _type.ToString().ToLower() + " text for " + parent.Name + "(" +
                                    parent.Guid.ToRQLString() + ")");
            }
            _hasChanged = false;
        }

        protected override void UpdateValue(string value)
        {
            _guid = !string.IsNullOrEmpty(value) ? new Guid(value) : new Guid();
        }

        public override bool Equals(object o)
        {
            var attr = o as TextContentAttribute;
            return attr != null && attr._type == _type && attr.Text == Text;
        }

        public override int GetHashCode()
        {
            return _type.GetHashCode() + 23*base.GetHashCode();
        }
    }
}