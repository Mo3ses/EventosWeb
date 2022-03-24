import { Evento } from "./Evento";

export interface Lote {
  id: number;
  nome: string;
  preco: number;
  dataInicip?: Date;
  dataFim?: Date;
  quantidade: number;
  eventoId: number;
  evento: Evento;
}
