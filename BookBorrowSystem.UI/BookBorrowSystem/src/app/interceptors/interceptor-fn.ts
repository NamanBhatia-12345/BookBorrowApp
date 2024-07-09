import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";


@Injectable()
export class InterceptorFn implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let modifiedReq = req;
    const token = this.authService.takeToken();
    if (token !== null) {
      modifiedReq = modifiedReq.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
    }
    return next.handle(modifiedReq);
  }
}

export const interceptorFnProviders = [
  {
    provide: HTTP_INTERCEPTORS,
    useClass: InterceptorFn,
    multi: true,
  },
];
