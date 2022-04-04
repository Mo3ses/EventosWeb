import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/Models/Lote';
import { Observable, take } from 'rxjs';

@Injectable()
export class LoteService {


  baseURL = 'https://localhost:5001/api/lotes';

  constructor(private http: HttpClient) { }

  public getEventos(): Observable<Lote[]>{
    return this.http.get<Lote[]>(this.baseURL)
    .pipe(take(1));
  }

  public getLotesByEventId(eventoId: number): Observable<Lote[]>{
    return this.http.get<Lote[]>(`${this.baseURL}/${eventoId}`)
    .pipe(take(1));
  }

  public saveLote(eventoId: number, lotes: Lote[]): Observable<Lote[]>{
    return this.http.put<Lote[]>(`${this.baseURL}/${eventoId}`, lotes)
    .pipe(take(1));
  }

  public deleteLote(eventoId: number, loteId: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${eventoId}/${loteId}`)
    .pipe(take(1));
  }



}
