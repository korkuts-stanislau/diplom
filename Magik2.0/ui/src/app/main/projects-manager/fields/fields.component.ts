import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Field } from 'src/models/resource/field';
import { ModalService } from 'src/services/modal/modal.service';
import { FieldsService } from 'src/services/field/fields.service';
import { ProjectsService } from 'src/services/project/projects.service';
import { Project } from 'src/models/resource/project';

@Component({
  selector: 'app-fields',
  templateUrl: './fields.component.html',
  styleUrls: ['./fields.component.css']
})
export class FieldsComponent implements OnInit {
  public defaultProjectColor = "#23a5d588";

  public fields?:Field[];

  public currentField?: Field;

  public currentFieldProjects: Project[] = new Array<Project>();

  @Output()private currentProjectChanged: EventEmitter<Project> = new EventEmitter<Project>();

  @Input()public currentProject?: Project;

  public isFieldsShown: boolean = true;

  constructor(private sanitizer: DomSanitizer,
    public modalService: ModalService,
    private fieldsService: FieldsService,
    private projectsService: ProjectsService) { }

  ngOnInit(): void {
    this.getFields();
  }

  selectField(field: Field) {
    this.currentField = field;
    this.getCurrentFieldProjects();
    this.hideFields();
  }

  showFields() {
    this.isFieldsShown = true;
  }

  hideFields() {
    this.isFieldsShown = false;
  }

  getUrlFromIcon(icon:string) {
    if(icon == "") return "assets/puzzle.jpg";
    return  this.sanitizer.bypassSecurityTrustResourceUrl(
        `data:image/png;base64, ${icon}`);
  }

  getFields() {
    this.fieldsService.getFields()
      .subscribe(res => {
        this.fields = res;
      },err => {
        alert("Не удалось получить данные с сервера");
        console.log(err);
      });
  }

  addFieldModal() {
    this.modalService.openModal('add-field');
  }

  addField(field: Field) {
    this.fieldsService.addField(field)
      .subscribe(res => {
        field.id = res;
        this.fields!.push(field);
      }, err => {
        console.log(err);
        alert("Добавление не удалось");
      });
  }

  editFieldModal() {
    this.modalService.openModal('edit-field');
  }

  editField(field: Field) {
    this.fieldsService.editField(field)
      .subscribe(res => {
        
      }, err => {
        console.log(err);
        alert("Изменение не удалось");
      });
  }

  deleteCurrentField() {
    if(confirm("Вы уверены что хотите удалить эту область проектов?")) {
      this.fieldsService.deleteField(this.currentField!)
        .subscribe(res => {
          this.fields = this.fields?.filter(f => f !== this.currentField!);
          this.currentField = undefined;
          this.showFields();
        }, err => {
          console.log(err);
          alert("Удаление не удалось");
        });
    }
  }

  getCurrentFieldProjects() {
    this.currentFieldProjects = new Array<Project>();
    this.projectsService.getProjects(this.currentField!)
      .subscribe(res => {
        this.currentFieldProjects = res;
      }, err => {
        console.log(err);
        alert("Получение проектов не удалось");
      })
  }

  addNewProject() {
    let newProject = new Project(0, "Новый проект", "Мой новый проект");
    this.projectsService.addProject(this.currentField!, newProject)
      .subscribe(res => {
        newProject.id = res;
        this.currentFieldProjects.push(newProject);
      }, err => {
        console.log(err);
        alert("Не удалось добавить проект");
      })
  }

  changeProject(project: Project) {
    this.currentProject = project;
    this.currentProjectChanged.emit(this.currentProject);
  }
}
