using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate;
using NHibernate.Linq;

namespace Chapter2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string connString =
        @"Data Source=MDDSK40044;Initial Catalog=NH3BeginnersGuide;Integrated Security=True;
Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public MainWindow()
        {
            InitializeComponent();
           
        }

       

        private ISessionFactory CreateSessionFactory()
        {
            return    Fluently.Configure()
                     .Database(MsSqlConfiguration
                     .MsSql2008
                     .ConnectionString(connString))
                     .Mappings(m => m.FluentMappings
                     .AddFromAssemblyOf<ProductMap>())
                     .BuildSessionFactory();
        }

        private void btnCreateDatabase_Click(object sender, RoutedEventArgs e)
        {
            
            Fluently.Configure()
            .Database(MsSqlConfiguration
            .MsSql2008
            .ConnectionString(connString))
            .Mappings(m => m.FluentMappings
            .AddFromAssemblyOf<ProductMap>())
            .ExposeConfiguration(CreateSchema)
            .BuildConfiguration();
        }

        private void btnCreateSessionFactory_Click(object sender, RoutedEventArgs e)
        {
            var factory = CreateSessionFactory();
        }

        private void btnCreateSession_Click(object sender, RoutedEventArgs e)
        {
            var factory = CreateSessionFactory();
            using (var session = factory.OpenSession())
            {
                // do something with the session
            }
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            var factory = CreateSessionFactory();
            using (var session = factory.OpenSession())
            {
                var category = new Category
                {
                    Name = txtCategoryName.Text,
                    Description = txtCategoryDescription.Text
                };
                session.Save(category);
            }
        }


        private void btnLoadCategories_Click(object sender, RoutedEventArgs e)
        {
            var factory = CreateSessionFactory();
            using (var session = factory.OpenSession())
            {
                var categories = session.Query<Category>()
                .OrderBy(c => c.Name)
                .ToList();
                lstCategories.ItemsSource = categories;
                lstCategories.DisplayMemberPath = "Name";
            }
        }

        private static void CreateSchema(Configuration cfg)
        {
            var schemaExport = new SchemaExport(cfg);
            schemaExport.Drop(false, true);
            schemaExport.Create(false, true);
        }
    }
}
