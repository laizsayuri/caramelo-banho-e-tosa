using System;
using System.Collections.Generic;
using Xamarin.Forms;
using CarameloApp.Models;
using CarameloApp.Views.Shared.Components;
using CarameloApp.Views.Shared;
using CarameloApp.Views.Shared.ContentAreas;

namespace CarameloApp.Views.Schedules.ListViews
{
	// listagem de agendamentos
	public class ScheduleListView : CustomListView<Schedule>
	{
		private Action<object, CheckedChangedEventArgs> _checkBoxEvent;

		public ScheduleListView(List<Schedule> schedules, bool useCheckbox = false, Action<object, CheckedChangedEventArgs> checkBoxEvent = null) : 
			base(schedules)
		{
			_checkBoxEvent = checkBoxEvent;
			SelectionMode = !useCheckbox ? ListViewSelectionMode.None : ListViewSelectionMode.Single;

			ItemTemplate = new DataTemplate(() =>
			{
				Label idLabel = new Label
				{
					IsVisible = false
				};
				idLabel.SetBinding(Label.TextProperty, "Id");

				CustomLabel dateLabel = new CustomLabel();
				dateLabel.SetBinding(Label.TextProperty, "DateTimeListAnotation");

				CustomLabel serviceLabel = new CustomLabel();
				serviceLabel.SetBinding(Label.TextProperty, "ServiceTypeDescription");

				CustomLabel statusLabel = new CustomLabel
				{
					Text = "Realizado",
					IsVisible = !useCheckbox
				};

				Image servicePic = new Image();
				servicePic.SetBinding(Image.SourceProperty, "PicFile");

				CheckBox checkBox = new CheckBox
				{
					IsVisible = useCheckbox,
				};
				checkBox.CheckedChanged += ConcludeScheduleCheckBox_CheckedChanged;

				return new ViewCell
				{
					View = new Card
					{
						Children =
						{
							servicePic,
							new CardCenterContent
							{
								Children =
								{
									dateLabel,
									serviceLabel,
									statusLabel
								}
							},
							new CardEndContent
							{
								Children = {
									idLabel,
									checkBox
								}
							}
						}
					},
				};				
			});
		}

		private void ConcludeScheduleCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			_checkBoxEvent(sender, e);
		}
	}
}
