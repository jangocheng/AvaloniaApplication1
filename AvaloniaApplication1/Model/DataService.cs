using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AvaloniaApplication1.Model
{
    static class DataService
    {
        static readonly string dataPath = Directory.GetCurrentDirectory() + @"\data.xml";
        static DataService()
        {
            if (!File.Exists(dataPath))
            {
                XDocument xDoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("ToDoItems"));
                xDoc.Save(dataPath);
                xDoc.Element("ToDoItems").Add(new XElement(
                       "ToDoItem",
                       new XAttribute("Guid", "F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"),
                       new XAttribute("DateTime", "2016-11-15T10:04:24.3452774+02:00"),
                       "Я не стал разбираться с проблемой блокировки ввода символов в TextBox. Чтобы проверить обновление, можно удалить пару символов (BackSpace работает)."));
                xDoc.Element("ToDoItems").Add(new XElement(
                       "ToDoItem",
                       new XAttribute("Guid", "e48a096c-cc3a-470a-8d08-06500188c487"),
                       new XAttribute("DateTime", "2016-11-16T12:44:14.3452774+02:00"),
                       "Строк кода немного, - комментарии не писал"));
                xDoc.Save(dataPath);
            }
        }
        public static void LoadToDoItems(Action<ObservableCollection<ToDoItem>, Exception> callback)
        {
            ObservableCollection<ToDoItem> toDoItems = null;
            Exception exeption = null;
            try
            {
                XDocument doc = XDocument.Load(dataPath);
                IEnumerable<XElement> elements = doc.Element("ToDoItems").Elements("ToDoItem");
                var items = from x in elements
                            select new ToDoItem
                            {
                                Guid = Guid.Parse(x.Attribute("Guid").Value),
                                DateTime = DateTime.Parse(x.Attribute("DateTime").Value),
                                ToDoText = x.Value
                            };

                toDoItems = new ObservableCollection<ToDoItem>(items);

            }
            catch (Exception ex)
            {
                exeption = ex;
            }
            callback(toDoItems, exeption);
        }

        internal static void DeleteToDoItem(Action<Exception> callback, ToDoItem toDoItem)
        {
            Exception exeption = null;
            try
            {
                XDocument doc = XDocument.Load(dataPath);
                doc.Element("ToDoItems").Elements("ToDoItem")
                    .Where(x => Guid.Parse(x.Attribute("Guid").Value) == toDoItem.Guid)
                    .Remove();
                doc.Save(dataPath);
            }
            catch (Exception ex)
            {
                exeption = ex;
            }
            callback(exeption);
        }

        internal static void AddOrUpdateToDoItem(Action<Exception> callback, ToDoItem toDoItem)
        {
            Exception exeption = null;
            try
            {
                XDocument doc = XDocument.Load(dataPath);
                var item = doc.Element("ToDoItems").Elements("ToDoItem")
                    .FirstOrDefault(x => Guid.Parse(x.Attribute("Guid").Value) == toDoItem.Guid);

                if (item == null)
                {
                    doc.Element("ToDoItems")
                        .Add(new XElement(
                            "ToDoItem",
                            new XAttribute("Guid", toDoItem.Guid),
                            new XAttribute("DateTime", toDoItem.DateTime),
                            toDoItem.ToDoText));
                }
                else
                {
                    item.Value = toDoItem.ToDoText;
                }
                doc.Save(dataPath);
            }
            catch (Exception ex)
            {
                exeption = ex;
            }
            callback(exeption);
        }
    }
}
