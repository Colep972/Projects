// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/GameModeBase.h"
#include "Blueprint/UserWidget.h"
#include "LooseMenuGameMode.generated.h"

/**
 * 
 */
UCLASS()
class ENTERTHEPOTATO_API ALooseMenuGameMode : public AGameModeBase
{
	GENERATED_BODY()
	
    public:
        virtual void BeginPlay() override;

    protected:
        UPROPERTY(EditDefaultsOnly, Category = "UI")
        TSubclassOf<class UUserWidget> LooseMenuWidgetClass;


    private:
        UPROPERTY()
        UUserWidget* LooseMenuWidget;
};
