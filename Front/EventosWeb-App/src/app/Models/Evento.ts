import { Lote } from "./Lote";
import { Palestrante } from "./Palestrante";
import { RedeSocial } from "./RedeSocial";

export interface Evento {

   id: number;//int
   local: string;
   dataEvento?: Date;
   tema:string;
   qtdPessoas: number;
   imagemURL: string;
   telefone: string;
   email: string;
   lotes: Lote[];
   redeSociais: RedeSocial[];
   palestrantesEventos: Palestrante[];
}
