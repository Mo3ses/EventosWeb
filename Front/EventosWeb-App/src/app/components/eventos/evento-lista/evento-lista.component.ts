import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '@app/Models/Evento';
import { EventoService } from '@app/Services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {



  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public eventoId = 0;

  modalRef: BsModalRef | undefined;
  public larguraImg = 9;
  public exibirImg = true;
  private filtroListado = '';

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void { //Metodo é chamado antes de carregar o HTML

    this.carregarEventos();
    this.spinner.show();
  }

  public toggleImagem(): void {
    this.exibirImg = !this.exibirImg;
  }

  public carregarEventos(): void {
    this.eventoService.getEventos().subscribe({
      next: (eventos: Evento[]) => {
        this.eventos = eventos;
        this.eventosFiltrados = this.eventos;
      },
      error: (error: any)=> {
        this.spinner.hide()
        this.toastr.error('Erro ao Carregar os Eventos.','Erro!')
      },
      complete: () => this.spinner.hide()
    }); //acessa a função do servidor do back
  }

  public get filtroLista(): string {
    return this.filtroListado;
  }
  public set filtroLista(value: string){
    this.filtroListado = value;
    this.eventosFiltrados =  this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (e:any) => e.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      e.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1);
  }

  openModal(event: any, template: TemplateRef<any>, eventoId: number) {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef!.hide();
    this.spinner.show();

    this.eventoService.deleteEvento(this.eventoId).subscribe(
      (result: any) => {
          if (result.message == 'Deletado'){
            console.log(result)
            this.toastr.success('Evento deletado com sucesso.', 'Deletado!');
            this.carregarEventos();
          }

      },
      (error: any) => {
        this.toastr.error(`Erro ao tentar deletar o Evento ${this.eventoId}`, 'Erro');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef?.hide();
    this.toastr.error('Ação foi cancelada com sucesso.','Ação Cancelada')
  }

  detalheEvento(id: number): void{
    this.router.navigate([`eventos/detalhe/${id}`])
  }

}
