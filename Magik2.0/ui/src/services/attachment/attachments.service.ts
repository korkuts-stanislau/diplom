import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RESOURCE_API_URL } from 'src/app/config/app-injection-tokens';
import { Attachment } from 'src/models/resource/attachment';
import { Stage } from 'src/models/resource/stage';

@Injectable({
  providedIn: 'root'
})
export class AttachmentsService {

  constructor(@Inject(RESOURCE_API_URL)private url:string, 
    private http:HttpClient) { }

    getAttachments(): Observable<Attachment[]> {
      return this.http.get<Attachment[]>(`${this.url}api/attachments`);
    }

    getAttachmentsForStage(stage: Stage): Observable<Attachment[]> {
      return this.http.get<Attachment[]>(`${this.url}api/attachments/stage/${stage.id}`);
    }

    createAttachment(attach: Attachment): Observable<Attachment> {
      return this.http.post<Attachment>(`${this.url}api/attachments`, attach);
    }

    addAttachmentToStage(stage: Stage, attach: Attachment): Observable<any> {
      return this.http.post<Attachment>(`${this.url}api/attachments/stage/${stage.id}`, attach.id);
    }

    editAttachment(attach: Attachment): Observable<Attachment> {
      return this.http.put<Attachment>(`${this.url}api/attachments/${attach.id}`, attach);
    }

    deleteAttachmentFromStage(stage: Stage, attachment: Attachment): Observable<any> {
      return this.http.delete<any>(`${this.url}api/attachments/stage/${stage.id}?attachmentId=${attachment.id}`);
    }

    deleteAttachment(attach: Attachment): Observable<any> {
      return this.http.delete<any>(`${this.url}api/attachments/${attach.id}`);
    }
}
