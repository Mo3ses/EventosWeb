import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from '@app/Models/Evento';
import { EventoService } from '@app/Services/evento.service';


@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  form!: FormGroup;
  evento = {} as Evento;
  modoSalvar = 'post';

  get f(): any{
    return this.form.controls;
  }

  get bsConfig(): any{
    return {dateInputFormat: 'DD-MM-YYYY hh:mm',
    isAnimated: true,
    adaptivePosition: true,
    containerClass: 'theme-default',
    showWeekNumbers: false,
    }
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private route: ActivatedRoute,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) {
      this.localeService.use('pt-br')
    }


  public carregarEvento(): void {
    const eventoIdParam = this.route.snapshot.paramMap.get('id');

    if(eventoIdParam != null){
      this.spinner.show();

      this.modoSalvar = 'put';

      this.eventoService.getEventoById(+eventoIdParam).subscribe(
        (evento: Evento) => {
          this.evento = {... evento};
          this.form.patchValue(this.evento);
        },
        (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao tentar carregar Evento');
          console.error(error);
        },
        () => {this.spinner.hide()},
      );
    }
  }

  ngOnInit(): void {
    this.carregarEvento();
    this.validation();
  }

  public validation(): void{
    this.form = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemURL: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  public resetForm():void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public salvarAlteracao(): void {
    this.spinner.show();
    if(this.form.valid){

      if(this.modoSalvar == "post"){
        this.evento = {... this.form.value};
      } else {
        this.evento = {id: this.evento.id, ... this.form.value};
      }

      this.eventoService[this.modoSalvar](this.evento).subscribe(
        () => {this.toastr.success('Evento Salvo com Sucesso!', 'Sucesso')},
        (error: any) => {
          this.spinner.hide();
          this.toastr.error('Error ao salvar o evento', 'Erro')
          console.error(error);
        },
        () => {this.spinner.hide()}
      );
    }
  }
}

