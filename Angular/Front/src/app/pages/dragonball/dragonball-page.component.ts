import { NgClass } from "@angular/common";
import { Component, computed, signal } from "@angular/core";
import { Character } from "../../interfaces/character.interface";


@Component({
    templateUrl: './dragonball-page.component.HTML',
    imports: [
        // NgClass
    ]

})
export class DragonballPageComponent{


    name = signal('');
    power = signal(0)


    
    characters = signal<Character[]>([
        {id: 1, name: 'Goku', power: 9001},
        // {id: 2, name: 'Vegeta', power: 8000},
        // {id: 4, name: 'Yancha', power: 500},
        // {id: 3, name: 'Piccoro', power: 3000}
        

    ]);

    // powerClass = computed(() =>{
    //     return{
    //         'text-darger': true,
    //     }
    // })

    addCharacter() {
        if (!this.name() || !this.power() || this.power() <= 0) {
            return
        }
        const newCharacter: Character = {
            id: this.characters().length +1,
            name: this.name(),
            power: this.power(),

        }

        this.characters.update((list) => [...list,newCharacter])
        this.resetFields();

        


    }
    resetFields(){
            this.name.set('');
            this.power.set(0);
        }
    
}