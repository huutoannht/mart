using System;
using System.Collections.Generic;
using System.Linq;
using Data.DataContract.CategoryDC;
using Data.DataContract.ProductDC;
using ML.Common;
using Share.Helper.Cache;
using StructureMap;

namespace Share.Helper
{
    public static partial class SiteUtils
    {
        public static List<Category> GetCategoryPath(List<Category> listNoTree, Guid cateId)
        {
            var list = new List<Category>();

            var child = listNoTree.FirstOrDefault(i => i.Id == cateId);
            if (child == null) return list;

            list.Add(child);

            var parent = GetCategoryParent(listNoTree, child);
            while (parent != null)
            {
                list.Add(parent);
                parent = GetCategoryParent(listNoTree, parent);
            }

            var listResult = new List<Category>();

            for (var i = list.Count - 1; i >= 0; --i)
            {
                listResult.Add(list[i]);
            }

            return listResult;
        }

        public static List<Category> GetCategoryPath(Guid categoryId)
        {
            var cache = ObjectFactory.GetInstance<ICacheHelper>();
            var listAllCateTree = cache.GetCategoryTreeList();
            var listAllCateHasChildsNoTree = new List<Category>();
            BuildCategoryListFromTree(listAllCateHasChildsNoTree, listAllCateTree);
            var listPath = GetCategoryPath(listAllCateHasChildsNoTree, categoryId);
            return listPath;
        }

        public static Category GetCategoryParent(IEnumerable<Category> listNoTree, Category child)
        {
            if (!child.ParentId.HasValue) return null;
            return listNoTree.FirstOrDefault(i => i.Id == child.ParentId.Value);
        }

        public static void GetCategoryChildIds(List<Guid> listResult, Guid parentId, List<Category> listNoTree = null)
        {
            if (listNoTree == null)
            {
                listNoTree = new List<Category>();
                var cache = ObjectFactory.GetInstance<ICacheHelper>();
                BuildCategoryListFromTree(listNoTree, cache.GetCategoryTreeList());
            }

            var parent = listNoTree.FirstOrDefault(i => i.Id == parentId);
            if (parent == null) return;

            if (!parent.Childs.Any()) return;
            foreach (var child in parent.Childs)
            {
                listResult.Add(child.Id);
                GetCategoryChildIds(listResult, child.Id, listNoTree);
            }
        }

        public static void BuildCategoryListFromTree(List<Category> listResult, List<Category> listTree)
        {
            if (!listTree.Any()) return;
            foreach (var item in listTree)
            {
                listResult.Add(item);
                BuildCategoryListFromTree(listResult, item.Childs);
            }
        }

        public static List<Category> BuildCategoryListFromTree()
        {
            var cache = ObjectFactory.GetInstance<ICacheHelper>();
            var listResult = new List<Category>();
            BuildCategoryListFromTree(listResult, cache.GetCategoryTreeList());

            return listResult;
        }

        public static List<Category> BuildCategoryTreeFromList(List<Category> listAllCate = null)
        {
            if (listAllCate == null)
            {
                var cache = ObjectFactory.GetInstance<ICacheHelper>();
                listAllCate = cache.GetCategories();
            }

            if (std.GetAppSetting(ConstKeys.CacheProvider).ToLower() == "asp")
            {
                listAllCate = listAllCate.Clone();
            }

            var listResult = new List<Category>();
            listResult.AddRange(listAllCate.Where(i => !i.ParentId.HasValue));

            for (var i = 0; i < listResult.Count; ++i)
            {
                var cate = listResult[i];
                cate.LevelTreeView = (i + 1).ToString();
            }

            Action<Category> appendChild = null;
            appendChild = parent =>
            {
                var childs = listAllCate.Where(i => i.ParentId.HasValue && i.ParentId.Value == parent.Id).ToList();
                for (var i = 0; i < childs.Count; ++i)
                {
                    var cate = childs[i];
                    cate.OrderNumber = i + 1;
                    cate.Level = parent.Level + 1;
                }

                childs.ForEach(i =>
                {
                    i.LevelTreeView = parent.LevelTreeView + "." + i.OrderNumber;
                    i.ParentLevelTreeView = parent.LevelTreeView;
                    i.LevelPadding = parent.LevelPadding + 5;
                    parent.Childs.Add(i);
                    appendChild(i);
                });
            };

            listResult.ForEach(i => appendChild(i));

            return listResult;
        }
    }
}
