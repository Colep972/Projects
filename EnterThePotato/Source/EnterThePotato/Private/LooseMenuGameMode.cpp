// Fill out your copyright notice in the Description page of Project Settings.


#include "LooseMenuGameMode.h"

void ALooseMenuGameMode::BeginPlay()
{
    Super::BeginPlay();

    if (LooseMenuWidgetClass)
    {
        LooseMenuWidget = CreateWidget<UUserWidget>(GetWorld(), LooseMenuWidgetClass);

        if (LooseMenuWidget)
        {
            LooseMenuWidget->AddToViewport();
        }
    }
}
