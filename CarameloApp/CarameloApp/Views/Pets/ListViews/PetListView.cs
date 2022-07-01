using System.Collections.Generic;
using Xamarin.Forms;
using CarameloApp.Models;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared;
using CarameloApp.Views.Shared.ContentAreas;

namespace CarameloApp.Views.Pets.ListViews
{
	// listagem de pets. Separado dessa forma, pode ser reutilizado quando precisa
	public class PetListView : CustomListView<Pet>
	{
		public PetListView(List<Pet> pets, bool useCheckbox = false) : base(pets)
		{
			SelectionMode = useCheckbox ? ListViewSelectionMode.None : ListViewSelectionMode.Single;

			ItemTemplate = new DataTemplate(() =>
			{
				CustomLabel nameLabel = new CustomLabel(15);
				nameLabel.SetBinding(Label.TextProperty, "Name");

				CustomLabel speciesLabel = new CustomLabel(10);
				speciesLabel.SetBinding(Label.TextProperty, "SpeciesDescription");

				CustomLabel sexLabel = new CustomLabel(10);
				sexLabel.SetBinding(Label.TextProperty, "SexDescription");

				Label sizeLabel = new CustomLabel(10);
				sizeLabel.SetBinding(Label.TextProperty, "SizeDescription");

				Image petPic = new Image();
				petPic.SetBinding(Image.SourceProperty, "PicFile");

				CheckBox checkBox = new CheckBox
				{
					IsVisible = useCheckbox
				};
				checkBox.SetBinding(CheckBox.IsCheckedProperty, "IsSelected");
				
				return new ViewCell
				{
					View = new Card
					{
						Children =
						{
							petPic,
							new CardCenterContent
							{
								Children =
								{
									nameLabel,
									speciesLabel,
									sexLabel,
									sizeLabel,
								}
							},
							new CardEndContent
							{
								Children = {
									checkBox
								}
							}
						}
					},
				};
			});
		}
	}
}
