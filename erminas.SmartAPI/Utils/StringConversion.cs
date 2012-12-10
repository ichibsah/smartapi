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
using System.Linq;
using erminas.SmartAPI.CMS;

namespace erminas.SmartAPI.Utils
{
    public static class StringConversion
    {
        /// <summary>
        ///   Converts a Guid to the format expected by the RedDot server. ALWAYS use this format when sending a Guid to the server.
        /// </summary>
        /// <param name="guid"> Guid to convert </param>
        /// <returns> String representation of the Guid in the format expected by a RedDot server </returns>
        public static string ToRQLString(this Guid guid)
        {
            //uppercase conversion is needed for SOME queries (RedDot server will show strange behaviour on them otherwise).
            return guid.ToString("N").ToUpperInvariant();
        }

        /// <summary>
        ///   Converts a bool value to the format expected by the RedDot server
        /// </summary>
        public static string ToRQLString(this Boolean value)
        {
            return value ? "1" : "0";
        }

        public static string RQLFormat(this string value, params object[] args )
        {
            var newArgs = from x in args select ConvertRQL(x);
            return string.Format(value, newArgs.ToArray());
        }

        private static object ConvertRQL(object o)
        {
            if (o is Guid)
            {
                return ((Guid) o).ToRQLString();
            }

            if (o is Boolean)
            {
                return ((Boolean) o).ToRQLString();
            }

            return o is IRedDotObject ? ((IRedDotObject) o).Guid.ToRQLString() : o;
        }
    }
}