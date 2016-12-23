using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Infragistics.Web.Mvc;

namespace SignalR.Controllers
{
    public static class DataGridHelper
    {
        public class CustomGridPaging : GridPaging
        {
            /// <summary>
            /// Gets or sets the custom total records count.
            /// </summary>
            /// <value>
            /// The custom total records count.
            /// </value>
            public int CustomTotalRecordsCount { get; set; }

            /// <summary>
            /// Transforms the data source.
            /// </summary>
            /// <param name="queryString">The query string.</param>
            /// <param name="grid">The grid.</param>
            /// <param name="queryable">The queryable.</param>
            public override void TransformDataSource(NameValueCollection queryString, IGridModel grid, out IQueryable queryable)
            {
                queryable = (IQueryable)grid.DataSource;
                TotalRecordsCount = CustomTotalRecordsCount;
            }

        }

        public static GridModel GetGridModel(IEnumerable<GridColumn> columns)
        {
            var model = new GridModel { AutoGenerateColumns = false, Width = "100%" };
            foreach (var column in columns)
            {
                model.Columns.Add(column);
            }

            return model;
        }

        public static GridModel AddRemotePagingSorting(this GridModel model, string dataSourceUrl, int pageSize, int currentPageIndex, int totalRecords, IEnumerable<ColumnSortingSetting> sortingSettings = null, int pageCountLimit = 5)
        {
            model.DataSourceUrl = dataSourceUrl;
            model.Features.Add(new CustomGridPaging
            {
                Type = OpType.Remote,
                PageSize = pageSize,
                CurrentPageIndex = currentPageIndex,
                CustomTotalRecordsCount = totalRecords,
                CurrentPageDropDownLeadingLabel = "Page",
                PrevPageLabelText = "previous",
                PageSizeDropDownLocation = "inpager",
                PageCountLimit = pageCountLimit
            });

            var remoteSortingFeature = new GridSorting
            {
                Type = OpType.Remote,
                CaseSensitive = true
            };

            if (sortingSettings != null)
            {
                foreach (var setting in sortingSettings)
                {
                    remoteSortingFeature.ColumnSettings.Add(setting);
                }

                model.Features.Add(remoteSortingFeature);
            }



            return model;
        }

        public static GridModel AddLocalSorting(this GridModel model, IEnumerable<ColumnSortingSetting> sortingSettings = null)
        {
            var remoteSortingFeature = new GridSorting
            {
                Type = OpType.Local,
                CaseSensitive = true
            };

            if (sortingSettings != null)
            {
                foreach (var setting in sortingSettings)
                {
                    remoteSortingFeature.ColumnSettings.Add(setting);
                }
            }

            model.Features.Add(remoteSortingFeature);

            return model;
        }

        public static GridModel AddMultiColumnHeaders(this GridModel model)
        {
            model.Features.Add(new GridMultiColumnHeaders());
            return model;
        }

        public static GridModel AddRowSelection(this GridModel model)
        {
            model.Features.Add(new GridRowSelectors
            {
                EnableRowNumbering = false,
                RowSelectorsColumnWidth = "15px"
            });
            model.Features.Add(new GridSelection());

            return model;
        }

        public static GridModel AddMultiRowSelection(this GridModel model)
        {
            model.Features.Add(new GridRowSelectors
            {
                EnableCheckBoxes = true,
                EnableRowNumbering = false,
                RowSelectorsColumnWidth = "42px"
            });
            model.Features.Add(new GridSelection
            {
                MultipleSelection = true
            });

            return model;
        }

        public static GridModel AddSingleRowSelection(this GridModel model, bool enableCheckBoxes = true)
        {
            model.Features.Add(new GridRowSelectors
            {
                EnableCheckBoxes = enableCheckBoxes,
                EnableRowNumbering = false,
                RowSelectorsColumnWidth = enableCheckBoxes ? "42px" : "0px"
            });
            model.Features.Add(new GridSelection());
            return model;
        }
    }
}