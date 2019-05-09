using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

namespace MagicWand
{
    [Transaction(TransactionMode.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData revit, ref string message, ElementSet elements)
        {
            // Get application and document objects
            UIApplication uiapp = revit.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            IList<Reference> pickedrefs = null;

            ICollection<ElementId> ids = null;

            Selection sel = null;

            try
            {

                // Have the user select elements
                sel = uidoc.Selection;
                pickedrefs = sel.PickObjects(ObjectType.Element, "Select Elements");
                // Use LINQ to get ElemendId of all References in pickedrefs
                ids = (from Reference r in pickedrefs select r.ElementId).ToList();

                // Print out items that have been selected
                using (Transaction trans = new Transaction(doc))
                {
                    StringBuilder sb = new StringBuilder();
                    trans.Start("Transaction");
                    if (pickedrefs != null && pickedrefs.Count > 0)
                    {
                        foreach (ElementId eid in ids)
                        {
                            Element elem = doc.GetElement(eid);
                            sb.Append("\n" + elem.Name);
                        }
                        TaskDialog.Show("Debug", "Items Selected : " + sb.ToString());
                    }
                    trans.Commit();
                }

            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException ex)
            {
                TaskDialog.Show("Debug", "ex is " + ex.Message);
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }

            return Result.Succeeded;
        }
    }

    public class CommonTools
    {
        public void HighlightElements()
        {
            return;
        }
    }
}
