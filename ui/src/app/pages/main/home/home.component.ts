import { Component, ViewChild } from '@angular/core'
import { ConfirmationModalComponent } from '@app/components'
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'
import { ChartConfiguration, ChartData, ChartEvent, ChartType } from 'chart.js'
import { BaseChartDirective } from 'ng2-charts';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: [
  ]
})
export class HomeComponent {

  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined

  modalResult: string
  items = ['rick', 'jeniffer', 'cynthia', 'linda', 'mark', 'rob', 'vanessa', 'kaitlyn', 'catherine', 'juliane', 'zoey', 'aaron', 'albert', 'louise']
  page: number

  chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    scales: {
      x: {},
      y: {
        min: 10
      }
    },
    plugins: {
      legend: {
        display: true
      }      
    }
  }

  chartType: ChartType = 'bar'
  chartPlugins = [
    
  ]

  chartData: ChartData<'bar'> = {
    labels: [ '2006', '2007', '2008', '2009', '2010', '2011', '2012' ],
    datasets: [
      { data: [ 65, 59, 80, 81, 56, 55, 40 ], label: 'Series A' },
      { data: [ 28, 48, 40, 19, 86, 27, 90 ], label: 'Series B' }
    ]
  };

  constructor(
    private modal: NgbModal
  ) {
    this.page = 1
    this.modalResult = ''
  }

  confirmation(): void {
    this.modal.open(ConfirmationModalComponent).result.then(result => this.modalResult = result ? 'closed with YES' : 'closed with NO')
  }

  trackId(i: number, id: string) {
    return `${i}_${id}`
  }
}
