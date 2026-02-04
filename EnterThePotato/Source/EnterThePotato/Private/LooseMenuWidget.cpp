// Fill out your copyright notice in the Description page of Project Settings.


#include "LooseMenuWidget.h"
#include "Components/Button.h"
#include "Kismet/GameplayStatics.h"
#include "EPGameInstance.h"
#include "EnterThePotato/EnterThePotatoPlayerController.h"

bool ULooseMenuWidget::Initialize()
{
    bool Success = Super::Initialize();
    if (!Success) return false;

    // Trouver les boutons par leur nom

    if (ReturnButton)
        ReturnButton->OnClicked.AddDynamic(this, &ULooseMenuWidget::OnReturnClicked);

	return true;
}

void ULooseMenuWidget::OnReturnClicked()
{
    UEPGameInstance* gameInstance = Cast<UEPGameInstance>(GetGameInstance());
    UE_LOG(LogTemp, Warning, TEXT("Bouton Jouer cliqué !"));
    UWorld* World = GetWorld();
    if (World && gameInstance)
    {
        gameInstance->LoadMenuLevel(World);
    }
}
