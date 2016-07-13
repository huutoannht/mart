  public JsonResult GetJson(JQueryDataTableParamModel param)
        {
            // check login
            currentUser = new CurrentUser(Session);

            int sortColumnIndex = param.ISortCol_0;
            string order = param.SSortDir_0;

            string orderBy = string.Empty;

            switch (sortColumnIndex)
            {
                case 0:
                    orderBy = "Name";
                    break;
                case 1:
                    orderBy = "Description";
                    break;
                case 2:
                    orderBy = "Code";
                    break;
                case 3:
                    orderBy = "CreateDate";
                    break;
            }

            List<Agency> list = new List<Agency>();
            int totalRecords = 0;

            int page = (param.IDisplayStart / param.IDisplayLength) + 1;
            int limit = param.IDisplayLength;

            // get list
            list = _agencyService.GetList(page, limit, false, param.SSearch, orderBy, order);
            foreach (var item in list)
            {
                item.UserType = currentUser.UserType;
            }

            totalRecords = _agencyService.Count(false, param.SSearch);

            // return jon datatable
            return Json(new
            {
                sEcho = param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete agency with idAgency
        /// </summary>
        /// <param name="id">curent id</param>
        /// <returns>data value</returns>
        [BinderAuthentication]
        public JsonResult DeleteJson(string id)
        {
            // check login
            currentUser = new CurrentUser(Session);
            JsonCollection result = new JsonCollection()
            {
                Version = "0.1",
                Result = false,
                ReturnData = string.Empty
            };

            // create service

            if (id == null)
            {
                result.ReturnData = "Id is null";
            }
            else
            {
                bool deleteResult = false;
                try
                {
                    deleteResult = _agencyService.Delete(id, "1");
                }
                catch (Exception ex)
                {
                    result.ReturnData = ex.ToString();
                }

                if (deleteResult)
                {
                    result.Result = true;
                    result.ReturnData = "Deleted!";
                }
                else
                {
                    result.Result = false;
                }
            }

            // Add user log
            this.userLogger = new UserLogger(
                this.currentUser.IdUser,
                this.currentUser.IdFacility,
                String.Format(Resources.Logs.LOG_DELETE_POST, this.currentUser.UserName, pageName, id, DateTime.Now),
                this.Url.Action(),
                ActionConstant.ACTION_DELETE_POST,
                Request.UserHostAddress);
            if (!this.userLogger.Write())
            {
                // Out error message insert log fail
                ViewBag.ErrorMessage = Resources.Messages.ERRMSG_WRITE_LOG;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
