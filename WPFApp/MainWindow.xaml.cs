using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObjects;
using Services;

namespace WPFApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IProductService iProductService;
    private readonly ICategoryService iCategoryService;
    public MainWindow()
    {
        InitializeComponent();
        iProductService = new ProductService();
        iCategoryService = new CategoryService();
    }
    public void LoadCategoryList()
    {
        try
        {
            var catList = iCategoryService.GetCategories();
            cboCategory.ItemsSource = catList;
            cboCategory.DisplayMemberPath = "CategoryName";
            cboCategory.SelectedValuePath = "CategoryId";

        }
        catch(Exception e)
        {
            MessageBox.Show(e.Message,"Error loading categories.");
        }
    }
    public void LoadProductList()
    {
        try
        {
            var productList = iProductService.GetProducts();
            dgData.ItemsSource = productList;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error loading categories.");
        }
        finally
        {
           resetInput();
        }       
    }
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        LoadCategoryList();
        LoadProductList();
    }
    private void btnCreate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Product p = new Product();
            p.ProductName = txtProductName.Text;
            p.UnitPrice = decimal.Parse(txtPrice.Text);
            p.UnitsInStock = short.Parse(txtUnitsInStock.Text);
            p.CategoryId = Int32 .Parse(cboCategory.SelectedValue.ToString());
            iProductService.SaveProduct(p);
            
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            LoadProductList();
        }
    }
    private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DataGrid dataGrid = sender as DataGrid;
        DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
        DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
        string id = ((TextBlock)RowColumn.Content).Text;
        Product product = iProductService.GetProductById(Int32.Parse(id));
        txtProductID.Text = product.ProductId.ToString();
        txtProductName.Text = product.ProductName;
        txtPrice.Text = product.UnitPrice.ToString();
        txtUnitsInStock.Text = product.UnitsInStock.ToString();
        cboCategory.SelectedValue = product.CategoryId;
    }
    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    private void btnUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (txtProductID.Text.Length > 0 )
            {
                Product p = new Product();
                p.ProductId = Int32.Parse(txtProductID.Text);
                p.ProductName = txtProductName.Text;
                p.UnitPrice = decimal.Parse(txtPrice.Text);
                p.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                p.CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString());
                iProductService.UpdateProduct(p);
            }
            else
            {
                MessageBox.Show("You must select a Product !");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            LoadProductList();
        }
    }
    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (txtProductID.Text.Length > 0)
            {

                Product p = new Product();
                p.ProductId = Int32.Parse(txtProductID.Text);
                p.ProductName = txtProductName.Text;
                p.UnitPrice = decimal.Parse(txtPrice.Text);
                p.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                p.CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString());
                iProductService.DeleteProduct(p);
            }
            else
            {
                MessageBox.Show("You must select a Product !");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            LoadProductList();
        }
    }
    private void resetInput()
    {
        txtProductID.Text = "";
        txtProductName.Text = "";
        txtPrice.Text = "";
        txtUnitsInStock.Text = "";
        cboCategory.SelectedIndex = 0;
    }

}