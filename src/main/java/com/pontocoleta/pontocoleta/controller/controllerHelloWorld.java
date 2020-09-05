package com.pontocoleta.pontocoleta.controller;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class controllerHelloWorld {
    
    @GetMapping(value = "/")
    public String getMethodName() {
        return "PontoColeta";
    }

}
