using System;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace FusionLOS
{
	public class ShoppingCart
	{
		public DateTime Date { get; set; }
		public double Amount { get; set; }
        public string Document { get; set; }
        public string Title { get; set; }
    }

	public class Section
	{ 
		public string Title { get; set; }
		public IEnumerable<ShoppingCart> List { get; set; }
	}

	public class ViewModel
	{
		public IEnumerable<Section> List { get; set; }
	}

	public class AccordionViewPage : ContentPage
	{
		public AccordionViewPage()
		{
			this.Title = "Loan Tracking";

			var template = new DataTemplate(typeof(DefaultTemplate));

			var view = new AccordionView(template);
			view.SetBinding(AccordionView.ItemsSourceProperty, "List");
			view.Template.SetBinding(AccordionSectionView.TitleProperty, "Title");
			view.Template.SetBinding(AccordionSectionView.ItemsSourceProperty, "List");

            view.BindingContext =
				new ViewModel
				{ 
					List = new List<Section> {
                        new Section
                        {
                            Title = "Required Documents",
                            List = new List<ShoppingCart> {
                                new ShoppingCart { Document = "Tax Return"},
                                new ShoppingCart { Document = "W2S"},
                                new ShoppingCart { Document = "1099"},
                                new ShoppingCart { Document = "Income Certificate"},
                            }
                        },
                        new Section
						{
							Title = "Order Credit",
							List = new List<ShoppingCart> {
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 }
							}
						},
						new Section
						{
							Title = "Order Approval",
							List = new List<ShoppingCart> {
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 }
							}
						},
						new Section
						{
							Title = "Processing",
							List = new List<ShoppingCart> {
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 }
							}
						},
						new Section
						{
							Title = "Underwriting",
							List = new List<ShoppingCart> {
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 }
							}
						},
						new Section
						{
							Title = "Closing",
							List = new List<ShoppingCart> {
								new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 }
							}
						}
					}
				};
			this.Content = view;
		}
	}

	public class App : Application
	{
		public App()
		{
			MainPage = new NavigationPage(new AccordionViewPage());
		}
	}
}
