import { UpperCasePipe } from "@angular/common";
import { ChangeDetectionStrategy, Component, computed, signal } from "@angular/core";

@Component({
    templateUrl: './hero-page.component.html',
    styleUrls: ['./hero-page.component.css'],
    imports: [UpperCasePipe],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeroPageComponent {
    name = signal('Ironman')
    age = signal(45)   

    heroDescription = computed(() =>{
        const description = `${this.name()} - ${this.age()}`
        console.log('Loque trae la descripción', description)
        return description
    })

    capitalizedName = computed(()=> this.name().toUpperCase())


    changeHero(){
        this.name.set('SpiderMan')
        this.age.set(22)
    }
    resetForm(){
        this.name.set('Ironman ')
        this.age.set(45)
    }

    changeAge(){
        this.age.set(60)
    }

}