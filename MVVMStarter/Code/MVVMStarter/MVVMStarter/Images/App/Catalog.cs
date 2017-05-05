﻿using System.Collections.Generic;
using System.Linq;
using MVVMStarter.Models.App;
using MVVMStarter.Models.Base;
using MVVMStarter.Persistency.Base;

namespace MVVMStarter.Images.App
{
    public class Catalog : CatalogBase<Image>
    {
        #region Model Singleton implementation
        private static Catalog _instance = null;

        public static Catalog Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new Catalog();
                return _instance;
            }
        }

        /// <summary>
        /// Use a hard-coded source
        /// </summary>
        private Catalog() : base(new CollectionBase<Image>(), new HardCodedSourceBase<Image>(new HardCodedObjects()))
        {
        }
        #endregion

        public List<Image> AllWithTag(string tag)
        {
            var tagFilter = new Filter<Image>(tag, (obj) => obj.ContainsTag(tag));
            tagFilter.On = true;

            AddFilter(tagFilter);
            List<Image> filteredImages = FilteredAll;
            RemoveFilter(tag);

            return filteredImages;
        }

        public List<string> AllTags
        {
            get
            {
                List<string> allTags = new List<string>();

                foreach (Image img in All)
                {
                    allTags.AddRange(img.Tags);
                }

                return allTags.Distinct().ToList();
            }
        }
    }
}